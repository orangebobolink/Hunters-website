export enum BackendEndpoints {
     GET_USER = 'auth/authorization/user',
     GET_USERS = 'auth/user',
     LOGIN = 'auth/authorization/login',
     REGISTER = 'auth/authorization/registration',
     REFRESH = 'auth/token/refresh',
     LOGOUT = 'auth/token/revoke',
     GET_ANIMALS = 'hunting/animal',
     GET_HUNTINGSEASSON = 'hunting/huntingSeason',
     GET_FEEDING = 'hunting/feeding',
     GET_RANGERS = 'auth/user/rangers',
}