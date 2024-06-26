import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter, map, startWith } from 'rxjs';
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
    },
    {
      id: 'transactions',
      label: 'Transactions',
      icon: 'timeline',
      path: 'transactions',
      condition: this.isSupplier,
    }
  ]

  get isSupplier() {
    return this.authService.user?.role === 'supplier';
  }

  constructor(private router: Router, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map((event) => (event as NavigationEnd).url),
      startWith(this.router.routerState.snapshot.url)
    ).subscribe((url: string) => {
      this.activeTab = url.split('profile/')[1];
    })
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
