import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from 'src/shared/services/auth/auth.service';
import { AppUser } from 'src/shared/types/userTypes';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isMenuOpen = false;
  user: AppUser | null = null;

  currentRoute: string = '/';

  constructor(private router: Router, protected authService: AuthService) {}

  ngOnInit() {
    this.currentRoute = this.router.routerState.snapshot.url;
    this.user = this.authService.user;

    this.router.events.pipe(filter(r=>r instanceof NavigationEnd)).subscribe(r=>{
      this.currentRoute = (r as NavigationEnd).url;
    });
  }

  navigateLogin() {
    this.router.navigate(['/auth/login']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  navigate(route: string) {
    this.router.navigate([route])
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
