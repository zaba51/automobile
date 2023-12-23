import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { MatIconModule } from '@angular/material/icon';
import { SharedComponentsModule } from 'src/shared/shared-components.module';
import { IndexComponent } from './index/index.component';
import { ContentListComponent } from './catalog/content-list/content-list.component';
import { DetailsComponent } from './details/details.component';
import { CatalogComponent } from './catalog/catalog.component';
import { FilterPanelComponent } from './filter-panel/filter-panel.component';
import { isAuthenticatedGuard } from 'src/app/guards/is-authenticated.guard';

const routes: Routes = [
  { 
    path: '',
    component:  HomeComponent,
    children: [
      {
        path: '',
        component: IndexComponent
      },
      {
        path: 'search',
        component: CatalogComponent
      },
      {
        path: 'details/:id',
        component: DetailsComponent,
        canActivate: [isAuthenticatedGuard],
      }
    ]
  }
];

@NgModule({
  declarations: [
    HomeComponent,
    SearchComponent,
    IndexComponent,
    ContentListComponent,
    DetailsComponent,
    CatalogComponent,
    FilterPanelComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedComponentsModule,
    MatIconModule,
  ]
})
export class HomeModule { }
