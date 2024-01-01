export interface User {
    id: number,
    name: string,
    email: string,
  }
  
  export interface AddUser {
    password: string,
    email: string,
  }
  
  export interface AppUser {
    sub: number,
    email: string,
    role: string,
    exp: number,
  }