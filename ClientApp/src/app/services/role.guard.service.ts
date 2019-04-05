// src/app/auth/scope-guard.service.ts

import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class RoleGuardService implements CanActivate {

    constructor(public auth: AuthService, public router: Router) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {

        const roles = (route.data as any).expectedRoles;
        debugger;
        if (!this.auth.isAuthenticated() || !this.auth.userHasRoles(roles)) {

            console.log("Not authorized");
            this.router.navigate(['']);
            return false;
        }
        return true;
    }

}