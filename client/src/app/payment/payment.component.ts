import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { PaymentService } from './payment.service';
import { ISummaryOrder } from '../shared/models/order';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit, OnDestroy {

  queryParamSubscription!: Subscription;

  orderSummary! : ISummaryOrder;

  constructor(private activatedRoute: ActivatedRoute, private paymentService: PaymentService){}

  ngOnDestroy(): void {
    this.queryParamSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.verifyPament();
  }

  verifyPament(){

    let trxref = "";

    this.queryParamSubscription = this.activatedRoute.queryParams.subscribe((route) => {
      trxref = route["trxref"]
    })

    this.paymentService.verifyPayment(trxref).subscribe((response) => {
      if(response){
        this.orderSummary = response
        console.log(response);
      }
    }, error => console.log(error));
  }

}
