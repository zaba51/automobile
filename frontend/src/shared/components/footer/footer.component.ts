import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  socialMedia = [
    {
      name: 'Facebook',
      href: ''
    },
    {
      name: 'Instragram',
      href: ''
    },
    {
      name: 'Youtube',
      href: ''
    },
    {
      name: 'Snapchat',
      href: ''
    },
    {
      name: 'Twitter',
      href: ''
    },
  ]

  additionalLinks = [
    {
      name: 'Home'
    },
    {
      name: 'Terms and service'
    },
    {
      name: 'Gmail'
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
