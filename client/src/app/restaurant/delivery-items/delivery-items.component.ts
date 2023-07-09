import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-delivery-items',
  templateUrl: './delivery-items.component.html',
  styleUrls: ['./delivery-items.component.css']
})
export class DeliveryItemsComponent{

  @Input() item!: string;

  @Input() image!: string;

}
