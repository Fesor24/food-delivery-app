import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { map, of, switchMap, timer } from 'rxjs';
import { IApiResponse } from 'src/app/shared/models/apiResponse';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  registerForm!: FormGroup

  constructor(private accountService: AccountService, private router: Router, private fb: FormBuilder){}

   ngOnInit(): void {
    this.createFormGroup()
  }

  createFormGroup(){
    this.registerForm = this.fb.group({
      email: [null,[
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
      ],
      [this.validateEmail()]
    ],
      password: [null, [Validators.required]],
    });
  }

  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe(() => {
      this.router.navigateByUrl('/');
    }, error => console.log(error));
  }

  validateEmail(): AsyncValidatorFn{
    return control => {
      return timer(500).pipe(
       switchMap(() => {
        if(!control.value){
          return of(null);
        }
        return this.accountService.checkEmailExist(control.value).pipe(
          map((response : IApiResponse<boolean, object, object>) => {
            return response.result ? {emailExists: true} : null
          })
        )
       })
      )
    }
  }

}
