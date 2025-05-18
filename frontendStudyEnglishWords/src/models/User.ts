interface IUser {
  userName?: string;
  password?: string;
  email?: string;
}

export class User implements IUser {
  constructor(user?: Partial<User>) {
    this.userName = user?.userName;
    this.password = user?.password;
    this.email = user?.email;
  }

  userName?: string;
  password?: string;
  email?: string;

}
