import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../../account/account.service';
import { inject } from '@angular/core'

export const authGuard: CanActivateFn = (route, state) => {

    const token = inject(AccountService).getToken();

    if (!token) {
      return inject(Router).createUrlTree(["/", "account"]);
    }
  
    return true;
  
};
