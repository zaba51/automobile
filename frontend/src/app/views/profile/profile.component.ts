import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from 'src/shared/services/auth/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  isDialogOpen = true;
  activeTab: string = '';

  navigationTabs = [
    {
      id: 'profile',
      label: 'Profile',
      icon: 'directions_car',
      path: '',
      condition: true,
    },
    {
      id: 'offer',
      label: 'Offer',
      icon: 'business',
      path: 'offer',
      condition: this.isSupplier,
    },
    {
      id: 'reservations',
      label: 'Reservations',
      icon: 'directions_car',
      path: 'reservations',
      condition: true,
    }
  ]

  get isSupplier() {
    return this.authService.user?.role === 'supplier';
  }

  constructor(private router: Router, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    this.activeTab = this.router.routerState.snapshot.url.split('profile/')[1];
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
