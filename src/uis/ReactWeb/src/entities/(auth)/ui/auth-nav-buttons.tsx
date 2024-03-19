import {NavigationMenuItem, NavigationMenuLink, NavigationMenuList, navigationMenuTriggerStyle} from '@/shared/ui';
import {Link} from 'react-router-dom';
import {useTranslation} from 'react-i18next';

const AuthNavButtons = () => {
    const { t, i18n } = useTranslation();

    return (
        <NavigationMenuList className="flex flex-row w-full items-center">
            <NavigationMenuItem>
                <Link to="/login" >
                    <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                        {t('header.auth.sign-in')}
                    </NavigationMenuLink>
                </Link>
            </NavigationMenuItem>
            <NavigationMenuItem>
                <Link to="/registration" >
                    <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                        Регистрация
                    </NavigationMenuLink>
                </Link>
            </NavigationMenuItem>
        </NavigationMenuList>
    );
};

export default AuthNavButtons;