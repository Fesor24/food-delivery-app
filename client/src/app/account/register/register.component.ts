import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  registerForm!: FormGroup

  constructor(){}

   ngOnInit(): void {
    this.createFormGroup()
  }

  createFormGroup(){
    this.registerForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    })
  }

  onSubmit(){
    console.log(this.registerForm.value);
  }

}
