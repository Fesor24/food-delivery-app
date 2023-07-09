import { IProducts } from "./product";

export interface IRestaurant{
  id:string,
  name: string,
  pictureUrl: string,
  address: string,
  deliveryFee: number,
  ratings: number,
  reviews: number,
  products: IProducts[]
}
