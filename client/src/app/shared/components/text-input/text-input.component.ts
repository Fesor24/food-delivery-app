import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements OnInit, ControlValueAccessor {

  @ViewChild('input', {static:true}) input!: ElementRef;

  @Input() type = 'text';

  @Input() label!: string;

  value = "";

  constructor(@Self() public controlDir: NgControl){
    this.controlDir.valueAccessor = this;
  }

   ngOnInit(): void {
    const control = this.controlDir.control;
    const validator = control?.validator ? [control?.validator] : [];
    const asyncValidator = control?.asyncValidator ? [control?.asyncValidator] : [];

    control?.setValidators(validator);

    control?.setAsyncValidators(asyncValidator);

    control?.updateValueAndValidity();
  }

  onChange = (event: any) => {
  };


  onTouched = () => {};

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }




}
