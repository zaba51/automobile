import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  isDialogOpen = true;
  activeTab: string = 'offer'

  navigationTabs = [
    {
      id: 'profile',
      label: 'Profile',
      icon: 'directions_car',
      path: ''
    },
    {
      id: 'offer',
      label: 'Offer',
      icon: 'business',
      path: ''
    },
    {
      id: 'reservations',
      label: 'Reservations',
      icon: 'directions_car',
      path: 'reservations'
    }
  ]

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    console.log(this.router, this.route)
  }

  onActionClick(action: any) {
    if (action.type == 'rent') {
      this.toggleModal(true);
    }
  }

  toggleModal(isOpen: boolean) {
    this.isDialogOpen = isOpen;
  }

  changeTab(tabId: string) {
    this.activeTab = tabId;

    const tab = this.navigationTabs.find(tab => tab.id === tabId);
    this.router.navigate([`/profile/${tab?.path}`])
  }
}
