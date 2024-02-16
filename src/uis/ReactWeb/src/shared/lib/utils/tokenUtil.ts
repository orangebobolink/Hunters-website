import Cookies from "cookies-ts"

export const setTokens = ({
                               refreshToken,
                               accessToken,
                               expires = 7,
                           }: {
    refreshToken: string;
    accessToken: string;
    expires?: number;
}) => {
    localStorage.setItem('accessToken', accessToken);

    const cookies = new Cookies()
    cookies.set('refreshToken', refreshToken, {
        expires: expires,
        secure: true,
    });
};

export const removeTokens = () => {
    localStorage.removeItem('accessToken');

    const cookies = new Cookies()
    cookies.remove('refreshToken');
};