import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BrowserXhr } from '@angular/http';

@Injectable({
  providedIn: 'root'
})
@Injectable()
export class ProgressService {
  //Subject is a class that derives from Observable that let you push new values to the Observable
  uploadProgress: Subject<any> = new Subject();
  downloadProgress: Subject<any> = new Subject();
}

@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr {

  constructor(private service: ProgressService) { super();};

  build(): XMLHttpRequest {
    let xhr: XMLHttpRequest = super.build();

    xhr.onprogress = (event) => {
      debugger;
      this.service.downloadProgress.next(this.createProgress(event));
    };

    xhr.upload.onprogress = (event) => {
      debugger;
      this.service.uploadProgress.next(this.createProgress(event));
    };
    return xhr;
  }

  private createProgress(event) {
    return {
      total: event.total,
      percentage: Math.round(event.lodaded / event.total * 100)
    }
  }
}
