using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using AutoMapper;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
            }
            _logger.LogInformation("Discount is retrived for product name :{productName}, Amount", request.ProductName);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var resp = await _repository.CreateDiscount(coupon);
            _logger.LogInformation("Discount is successfully created. Product Name: {ProductName}", request.Coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var resp = await _repository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is successfully updated. Product Name: {ProductName}", request.Coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {

            var resp = await _repository.DeleteDiscount(request.Id.ToString());
            _logger.LogInformation("Discount is successfully deleted. Product Id: {Id}", request.Id);

            DeleteDiscountResponse response = new DeleteDiscountResponse()
            {
                Success = resp
            };
            return response;
        }
    }
}
