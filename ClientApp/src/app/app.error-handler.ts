import { ToastrService } from 'ngx-toastr';
import { ErrorHandler, Inject, NgZone } from "@angular/core";

export class AppErrorHandler implements ErrorHandler{
    constructor(
        private ngZone: NgZone,
        @Inject(ToastrService) private toastr: ToastrService){

    }
    handleError(error: any): void{
        this.ngZone.run(() => {
            this.toastr.error('Crazy error','Error');
            console.log(error);
        });
    }
}