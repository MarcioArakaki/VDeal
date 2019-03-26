import { AuthService } from './../services/auth.service';
import { Vehicle } from './../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { SaveVehicle } from '../models/vehicle';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  vehicles: Vehicle[];
  makes: any[];
  query: any = {
    pageSize: this.PAGE_SIZE,
  };
  allVehicles: Vehicle[];
  columns = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    {}
  ]

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
  ) { }

  ngOnInit() {

    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);
      
    this.populateVehicles();

    //DELETE THIS LATER
    // this.authService.login();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateVehicles();
  }

  onFilterChangeClient() {

    var vehicles = this.allVehicles;

    //For each filter
    if (this.query.makeId)
      vehicles = vehicles.filter(v => v.make.id == this.query.makeId);
    if (this.query.modelId)
      vehicles = vehicles.filter(v => v.model.id == this.query.modelId);

    this.vehicles = vehicles;
  }

  resetFilter() {
    this.query = {
      page : 1,
      pageSize: this.PAGE_SIZE,
    };
    this.populateVehicles();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateVehicles();
  }

}
