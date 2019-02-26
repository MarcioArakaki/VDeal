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
  vehicles: Vehicle[];
  makes: any[];
  filter: any = {};
  allVehicles: Vehicle[];

  constructor(
    private vehicleService: VehicleService,
  ) { }

  ngOnInit() {

    this.vehicleService.getMakes()
    .subscribe(makes => this.makes = makes);

    this.vehicleService.getVehicles()
      .subscribe(vehicles => this.vehicles = this.allVehicles =vehicles);


  }

  onFilterChange(){
    var vehicles = this.allVehicles;

    //For each filter
    if (this.filter.makeId)
      vehicles = vehicles.filter( v => v.make.id == this.filter.makeId);
    if (this.filter.modelId)
      vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);

    this.vehicles = vehicles;
  }

  resetFilter(){
    this.filter = {};
    this.onFilterChange();
  }

}
