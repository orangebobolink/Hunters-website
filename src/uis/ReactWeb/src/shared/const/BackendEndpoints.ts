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
     GET_RANGERS = 'auth/user/ranger',
     GET_LANDS = 'hunting/land',
     GET_PRODUCTS = 'hunting/product',
     GET_RAIDS = 'hunting/raid',
     GET_PERMISSION = 'hunting/permission'
}