import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent implements OnInit {
  @Input() title: string = '';
  @Input() icon: string = '';
  @Input() size: 'small' | 'medium' | 'big' = 'medium';
  @Output() outClick = new EventEmitter(); 

  constructor() { }

  ngOnInit(): void {
  }

}
