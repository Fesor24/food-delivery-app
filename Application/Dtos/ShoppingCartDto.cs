namespace Application.Dtos
{
    public class ShoppingCartDto
    {
        public string Id { get; set; }

        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
