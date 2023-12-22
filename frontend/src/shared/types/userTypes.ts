export interface User {
    id: number,
    name: string,
    email: string,
  }
  
  export interface AddUser {
    name: string,
    email: string,
  }
  
  export interface AppUser {
    sub: number,
    email: string,
    role: string,
    exp: number,
  }