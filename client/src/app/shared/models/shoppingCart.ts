import { v4 as uuidv4 } from 'uuid';

export interface IShoppingCart{
  id: string;
  items: IShoppingCartItem[]
}

export interface IShoppingCartItem{
  id: string;
  name: string;
  price: number;
  restaurantId: string;
  pictureUrl: string;
  quantity: number;
}

export class ShoppingCart implements IShoppingCart{
  id = uuidv4();
  items: IShoppingCartItem[] = [];

}

export interface IShoppingCartTotals{
  quantity: number;
  subtotal: number;
  delivery: number;
  total: number;
}
