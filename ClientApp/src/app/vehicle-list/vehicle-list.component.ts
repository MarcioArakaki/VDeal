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

  constructor(
    private vehicleService: VehicleService,
  ) { }

  ngOnInit() {
    this.vehicleService.getVehicles()
      .subscribe(vehicles => this.vehicles = vehicles);

      this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);
  }

}
