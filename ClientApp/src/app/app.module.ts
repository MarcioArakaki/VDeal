import { RoleGuardService } from './services/role.guard.service';
import { AuthGuardService } from './services/auth.guard.service';
import { AuthService } from './services/auth.service';
import { BrowserXhr } from '@angular/http';
import { AppErrorHandler } from './app.error-handler';

import { VehicleService } from './services/vehicle.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { VehicleViewComponent } from './vehicle-view/vehicle-view.component';
import { PhotoService } from './services/photo.service';
import { ProgressService, BrowserXhrWithProgress } from './services/progress.service';
import { NgProgressModule} from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { AdminComponent } from './admin/admin.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    VehicleViewComponent,
    AdminComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    NgProgressModule,
    NgProgressHttpModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '',redirectTo:'vehicles', pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'vehicles/new', component: VehicleFormComponent},
      { path: 'vehicles/:id', component: VehicleViewComponent},
      { path: 'vehicles/edit/:id', component: VehicleFormComponent},
      { path: 'vehicles', component: VehicleListComponent},
      { path: 'vehicle-page/:id', component: VehicleViewComponent},
      { path: 'admin', component: AdminComponent, canActivate: [RoleGuardService], data:{expectedRoles: ['admin']}}      
    ])
  ],
  providers: [
    {provide: ErrorHandler, useClass: AppErrorHandler},
    {provide: BrowserXhr, useClass: BrowserXhrWithProgress},
    {provide: BrowserXhr, useClass: BrowserXhrWithProgress},
    AuthService,
    VehicleService,
    PhotoService,
    ProgressService,  
    RoleGuardService  
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
