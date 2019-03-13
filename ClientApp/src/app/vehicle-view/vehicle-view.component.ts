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
  vehicle: any;
  vehicleId: number;

  constructor(
    private vehicleService: VehicleService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicle <= 0){
        router.navigate(['/vehicles']);
        return;
      }
    })
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(v => this.vehicle = v, err => {
        console.log(err);
      });
  }


  savePhotos(photos: FileList) {

  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/']);
        });
    }
  }

  redirect(){
    this.router.navigate(['/vehicles/edit/',this.vehicle.id]);
  }
}
