import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent implements OnInit {
  @Input() title: string = '';
  @Input() icon: string = '';
  @Input() size: 'small' | 'medium' | 'big' = 'medium';
  @Input() iconButton = false;
  @Input() style = 'default';
  @Output() outClick = new EventEmitter(); 

  constructor() { }

  ngOnInit(): void {
  }
  
}
