export enum Gender {
  Unknown = 0,
  Male = 1,
  Female = 2,
  Other = 3,
}

export enum UserRole {
  User = 0,
  Admin = 1,
}

export interface UserProfileResponse {
  userId: string
  firstName: string
  lastName: string
  phoneNumber?: string
  avatarUrl?: string
  gender: Gender
}

export interface UpsertProfileRequest {
  userId: string
  firstName: string
  lastName: string
  phoneNumber: string
  gender: Gender
}
