import { IShoppingCartItem } from "./shoppingCart";

export interface IOrder{
  cartId: string;
  address: IAddress
  callbackUrl: string
}

export interface IAddress{
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  state: string;
}

export interface ISummaryOrder {
  id: string;
  customerEmail: string;
  dateCreated: string;
  deliveryCharges: number;
  paymentStatus: string;
  status: string;
  subTotal: number;
  total: number;
  deliveryAddress: IAddress;
  orderItems: IOrderSummaryItem[];
}

export interface IOrderSummaryItem{
  productName: string;
  pictureUrl: string;
  quantity: number;
  price:number;
}
