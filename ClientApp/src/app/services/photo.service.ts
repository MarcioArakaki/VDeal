import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http: HttpClient) { }

  upload(vehicleId,file){
    var formData = new FormData();
    formData.append('file',file); //file is the name of the expected parameter
    return this.http.post(`/api/vehicles/${vehicleId}/photos`,formData);
  }

  getPhotos(vehicleId){
    return this.http.get(`/api/vehicles/${vehicleId}/photos`)
  }
}
