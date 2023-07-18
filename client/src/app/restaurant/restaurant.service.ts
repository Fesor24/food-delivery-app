import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { IApiResponse } from '../shared/models/apiResponse';
import { IRestaurant } from '../shared/models/restaurant';
import { BehaviorSubject, map } from 'rxjs';
import { IProducts } from '../shared/models/product';
import { IShoppingCart, IShoppingCartItem, IShoppingCartTotals, ShoppingCart } from '../shared/models/shoppingCart';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  baseUrl = environment.apiUrl

  private shoppingCartSource : BehaviorSubject<IShoppingCart | null> = new BehaviorSubject<IShoppingCart | null>(
    null
  )

  shoppingCart$ = this.shoppingCartSource.asObservable();

  private shoppingCartTotalSource: BehaviorSubject<IShoppingCartTotals> = new BehaviorSubject<IShoppingCartTotals>({
    delivery: 0,
    subtotal: 0,
    quantity: 0,
    total: 0
  })

  shoppingCartTotal$ = this.shoppingCartTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getRestaurantByLocation(location: string){

    let params = new HttpParams();

    params = params.append('location', location);

    return this.http.get<IApiResponse<IRestaurant[], object, object>>(this.baseUrl + 'restaurants', {params: params})
    .pipe(
      map((response: IApiResponse<IRestaurant[], object, object>) => response)
    );
  }

  getFoodsByRestaurant(restaurantId: string){
    let params = new HttpParams();

    params = params.append('restaurantId', restaurantId);

    return this.http.get<IApiResponse<IProducts[], object, object>>(this.baseUrl + 'products', {params:params})
    .pipe(
      map(response => response)
    )
  }

  getShoppingCart(shoppingCartId: string){
    return this.http
      .get<IApiResponse<IShoppingCart, object, object>>(
        this.baseUrl + 'shopping-cart?shoppingCartId=' + shoppingCartId
      )
      .pipe(
        map((response: IApiResponse<IShoppingCart, object, object>) => {
          this.shoppingCartSource.next(response.result);
          this.calculateShoppingCartTotals();
        })
      );
  }

  updateShoppingCart(shoppingCart: IShoppingCart){
    return this.http.post<IApiResponse<IShoppingCart, object, object>>(this.baseUrl + "shopping-cart", shoppingCart)
    .subscribe((response) => {
      this.shoppingCartSource.next(response.result)
      this.calculateShoppingCartTotals();
      console.log(response);
    }, error => console.log(error));
  }

  getShoppingCartValue(){
    return this.shoppingCartSource.value;
  }

  addItemToCart(item: IProducts, quantity = 1){
    const shoppingCartItem = this.mapProductToCartItem(item, quantity);

    const shoppingCart = this.getShoppingCartValue() ?? this.createShoppingCart();

    const updatedShoppingCart = this.addOrUpdateShoppingCart(shoppingCart, quantity, shoppingCartItem);

    this.updateShoppingCart(updatedShoppingCart);

  }

  incrementCartItemQuantity(item: IShoppingCartItem){
    const cart = this.getShoppingCartValue();

    if(!cart){
      return;
    }

    const itemIndex = cart.items.findIndex(x => x.id == item.id);

    cart.items[itemIndex].quantity++;

    this.updateShoppingCart(cart);
  }

  decrementCartItemQuantity(item: IShoppingCartItem){
    const cart = this.getShoppingCartValue();

    if(!cart){
      return;
    }

    const itemIndex = cart.items.findIndex(x => x.id === item.id);

    if(cart.items.some(x => x.id === item.id)){
      if(cart.items[itemIndex].quantity > 1){
        cart.items[itemIndex].quantity--;
        this.updateShoppingCart(cart);
      }
      else{
        this.removeItemFromCart(cart.items[itemIndex]);
      }
    }

    console.log(cart);
  }

  removeItemFromCart(item: IShoppingCartItem){

    const cart = this.getShoppingCartValue();

    if(!cart){
      return;
    }

     cart.items = cart.items.filter((x) => x.id !== item.id);

     if (cart.items.length > 0) {
       this.updateShoppingCart(cart);
     } else {
       this.deleteShoppingCart(cart);
     }
  }

  deleteShoppingCart(cart: IShoppingCart){
    return this.http
      .delete(this.baseUrl + 'shopping-cart?shoppingCartId=' + cart.id)
      .subscribe(
        () => {
          this.shoppingCartSource.next(null);
          this.shoppingCartTotalSource.next({
            quantity: 0,
            total: 0,
            delivery: 0,
            subtotal: 0,
          });
          localStorage.removeItem('cart_id');
        },
        (error) => {
          console.log(error);
        }
      );
  }

  private addOrUpdateShoppingCart(shoppingCart: IShoppingCart, quantity: number, itemToAdd: IShoppingCartItem)
  : IShoppingCart
  {
    const index = shoppingCart.items.findIndex(x => x.id === itemToAdd.id);

    if(index === -1){
      shoppingCart.items.push(itemToAdd);
    }else{
      shoppingCart.items[index].quantity += quantity;
    }

    return shoppingCart;
  }

  private createShoppingCart() : IShoppingCart{
    const shoppingCart = new ShoppingCart();

    localStorage.setItem('cart_id', shoppingCart.id);

    return shoppingCart;
  }

  private mapProductToCartItem(product: IProducts, quantity = 1): IShoppingCartItem{
    return{
      id: product.id,
      pictureUrl: product.pictureUrl,
      restaurantId: product.restaurantId,
      price: product.price,
      name: product.name,
      quantity
    }
  }

  private calculateShoppingCartTotals(){
    const basket = this.getShoppingCartValue();
    const delivery = 0;
    const subtotal = Number(basket && basket.items.reduce((accumulator, currentValue) =>
    (currentValue.price * currentValue.quantity) + accumulator, 0));
    const quantity = Number(basket && basket.items.reduce((accumulator, currentValue) =>
    currentValue.quantity + accumulator, 0));

    const total = subtotal + delivery;

    this.shoppingCartTotalSource.next({delivery, subtotal, quantity, total})
  }

}
