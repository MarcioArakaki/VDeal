import { VehicleService } from './../services/vehicle.service';
import { Vehicle } from './../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html',
  styleUrls: ['./vehicle-view.component.css']
})
export class VehicleViewComponent implements OnInit {
  vehicle : Vehicle;

  constructor(
    private vehicleService: VehicleService,
    private route: ActivatedRoute,
    private router: Router,
    ) {  
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'];
    })  
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicle.id)
    .subscribe(v => this.vehicle = v);
  }

  

  



}
