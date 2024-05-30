export interface LoginResponse {
    id: string
    username: string
    roles: string[]
    huntingLicenseId: string
    isPaid: boolean
    accessToken: string,
    avatarUrl: string
}