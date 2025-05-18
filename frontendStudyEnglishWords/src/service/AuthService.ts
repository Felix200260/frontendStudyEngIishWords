import { User, UserManager } from "oidc-client";
// import type { User } from '@/models/User';

const identityUrl = "http://10.147.19.66:2000";

export class AuthService {
  private userManager: UserManager;

  constructor(identityAddress: string) {
    const settings = {
      authority: identityAddress,
      client_id: "MedInfoCenter.WebApp",
      redirect_uri: `${window.location.origin}/signin-callback`,
      silent_redirect_uri: `${window.location.origin}/silent-callback`,
      automaticSilentRenew: true,
      response_type: "id_token token",
      scope: "openid profile worker",
      post_logout_redirect_uri: `${window.location.origin}`,
      filterProtocolClaims: true,
    };

    this.userManager = new UserManager(settings);
  }

  public getUser(): Promise<User | null> {
    return this.userManager.getUser();
  }

  public login(): Promise<void> {
    const state = window.location.href;
    if (state) {
      return this.userManager.signinRedirect({ state: window.location.href });
    }
    return this.userManager.signinRedirect();
  }

  public logout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

  public async getAccessToken(): Promise<string> {
    const user = await this.userManager.getUser();
    return user?.access_token ?? "";
  }

  public async silentRenew(currentWorkerId: number): Promise<void> {
    await this.userManager.signinSilent({
      extraQueryParams: {
        current_worker_id: currentWorkerId,
      },
    });
  }

  public renewToken(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public getIdentityUrl(): string | undefined {
    return this.userManager.settings.authority;
  }

  public signinRedirectCallback() {
    return this.userManager.signinRedirectCallback();
  }

  public signinSilentCallback() {
    return this.userManager.signinSilentCallback();
  }
}

export default new AuthService(identityUrl);
