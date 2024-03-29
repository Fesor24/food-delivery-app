import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm!: FormGroup;

  returnUrl!: string;

  constructor(private accountService: AccountService, private router: Router,
    private activatedRoute: ActivatedRoute){}

  ngOnInit(): void {
    this.createLoginForm();
    this.returnUrl = this.activatedRoute.snapshot.queryParams?.['returnUrl'] || '';
  }

  createLoginForm(){
    this.loginForm = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]
      ),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe((response) => {
      this.router.navigateByUrl(this.returnUrl);
      console.log("User logged in");
    }, error => console.log(error));
  }

}
