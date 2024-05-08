export interface LoginResponse {
    id: string
    username: string
    roles: string[]
    isPaid: boolean
    accessToken: string
}