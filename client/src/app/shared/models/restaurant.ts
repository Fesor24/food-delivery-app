import { IProducts } from "./product";

export interface IRestaurant{
  name: string,
  pictureUrl: string,
  address: string,
  deliveryFee: number,
  products: IProducts[]
}
