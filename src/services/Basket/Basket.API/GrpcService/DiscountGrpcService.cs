using Discount.Grpc.Protos;

namespace Basket.API.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient)
        {
            this._discountProtoServiceClient = _discountProtoServiceClient;
        }
        public async Task<CouponModel> GetDiscount(string prouctName)
        {
            var discountRequest = new GetDiscountRequest() { ProductName = prouctName };
            var resp= await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
            return resp;
        }
    }
}
