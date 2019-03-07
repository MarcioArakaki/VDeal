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
  queryResult: any = {};
  vehicles: Vehicle[];
  makes: any[];
  query: any = {
    pageSize: 3
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
  ) { }

  ngOnInit() {

    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
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
    this.query = {};
    this.onFilterChange();
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
