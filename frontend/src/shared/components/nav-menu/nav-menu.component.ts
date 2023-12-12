import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isMenuOpen = false;

  currentRoute: string = '/';

  constructor(private router: Router) {}

  ngOnInit() {
    this.currentRoute = this.router.routerState.snapshot.url;

    this.router.events.pipe(filter(r=>r instanceof NavigationEnd)).subscribe(r=>{
      this.currentRoute = (r as NavigationEnd).url;
    });
  }


  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  goToProfile() {
    this.router.navigate(['/profile'])
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
