import {
    NavigationMenu,
    NavigationMenuItem,
    NavigationMenuLink,
    NavigationMenuList,
    navigationMenuLogoTriggerStyle,
    navigationMenuTriggerStyle
} from '@/shared/ui/navigation-menu.tsx';
import {Link} from 'react-router-dom';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {useTranslation} from 'react-i18next';
import {MenuIcon} from 'lucide-react';
import {Button} from '@/shared/ui';
import {Sheet, SheetContent, SheetTrigger} from '@/shared/ui/sheet.tsx';
import logo from '@/assets/logo.png';
import ChatNavItem from '@/entities/chat/ui/chat-nav-item.tsx';
import LanguageNavItem from '@/shared/ui/custom/language-nav-item.tsx';
import ThemeNavItem from '@/shared/ui/custom/theme-nav-item.tsx';
import ProfileAvatarNavigate from '@/entities/user/ui/profile-avatar-navigate.tsx';
import AuthNavButtons from '@/entities/(auth)/ui/auth-nav-buttons.tsx';

export function Head() {
    const { isAuth, roles } = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "header.nav"
        });

    return (
        <header className="flex h-20 w-full shrink-0 items-center z-10 px-4 md:px-6 select-none">
            <div className="flex flex-row w-full fixed top-0 left-0 right-0 z-10 m-2">
                <Sheet>
                    <SheetTrigger asChild>
                        <Button className="lg:hidden" size="icon" variant="outline">
                            <MenuIcon className="h-6 w-6" />
                            <span className="sr-only">Toggle navigation menu</span>
                        </Button>
                    </SheetTrigger>
                    <SheetContent side="left">
                        <Link to='/'>
                            <img src={logo}
                                 className='size-[3rem]'
                                 alt='logo'/>
                        </Link>
                        <div className='grid gap-2 py-6'>
                            {(roles.includes("User") || roles.length == 0)
                                &&
                                <>
                                    <Link to="/trip" className="flex w-full items-center py-2 text-lg font-semibold">
                                        {t("trip")}
                                    </Link>
                                    <Link to="/paymantFee"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("paymantFee")}
                                    </Link>
                                    <Link to="/checkHuntingLicense"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("checkHuntingLicense")}
                                    </Link>
                                    <Link to="/rent"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("rent.tittle")}
                                    </Link>
                                </>
                                }
                            {roles.includes("Admin") &&
                                <>
                                    <Link to="/managing"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("managing")}
                                    </Link>
                                    <Link to="/rent"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("rent.tittle")}
                                    </Link>
                                </>
                            }
                            {roles.includes("Manager") &&
                                <>
                                    <Link to="/trip"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("trip")}
                                    </Link>
                                    <Link to="/permission"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("permission")}
                                    </Link>
                                    <Link to="/feeding"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("feeding")}
                                    </Link>
                                    <Link to="/rent"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("rent.tittle")}
                                    </Link>
                                    <Link to="/animal"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("animal")}
                                    </Link>
                                    <Link to="/reporting"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("reporting")}
                                    </Link>
                                </>
                            }
                            {roles.includes("Ranger") &&
                                <>
                                    <Link to="/trip"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("trip")}
                                    </Link>
                                    <Link to="/raid"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("raid")}
                                    </Link>
                                    <Link to="/feeding"
                                          className="flex w-full items-center py-2 text-lg font-semibold">
                                            {t("feeding")}
                                    </Link>
                                </>
                            }
                            {roles.includes("Director")  &&
                                <>
                                        <Link to="/reporting"
                                              className="flex w-full items-center py-2 text-lg font-semibold">
                                                {t("reporting")}
                                        </Link>
                                </>
                            }
                        </div>
                    </SheetContent>
                </Sheet>
                <NavigationMenu className="lg:hidden fixed right-0">
                    <NavigationMenuList className="w-full">
                        {
                            isAuth
                            ? <ChatNavItem/>
                            : <div></div>
                        }
                        <LanguageNavItem/>
                        <ThemeNavItem/>
                        {
                            isAuth
                            ? <ProfileAvatarNavigate/>
                            : <AuthNavButtons/>
                        }
                    </NavigationMenuList>
                </NavigationMenu>
            </div>
            <div className="flex flex-row fixed top-0 left-0 right-0 lg:mt-2 z-10 shadow-md lg:pb-2">
                <NavigationMenu className="hidden lg:flex lg:justify-between lg:mx-5">
                    <NavigationMenuList>
                        <NavigationMenuLink className={navigationMenuLogoTriggerStyle()}>
                            <img src={logo}
                                 className='size-[3rem]'
                                 alt='logo'/>
                            {t('webname')}
                        </NavigationMenuLink>
                        {(roles.includes("User") || roles.length == 0)
                            &&
                            <>
                                <NavigationMenuItem>
                                    <Link to="/trip" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("trip")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/paymantFee" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("paymantFee")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/checkHuntingLicense" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("checkHuntingLicense")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/rent" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("rent.tittle")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                            </>
                        }
                        {roles.includes("Admin") &&
                            <>
                                <NavigationMenuItem>
                                    <Link to="/managing" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("managing")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/rent" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("rent.tittle")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                            </>
                        }
                        {roles.includes("Manager") &&
                            <>
                                <NavigationMenuItem>
                                    <Link to="/trip" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("trip")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/permission" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("permission")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/feeding" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("feeding")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/rent" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("rent.tittle")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/animal" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("animal")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/reporting" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("reporting")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                            </>
                        }
                        {roles.includes("Ranger") &&
                            <>
                                <NavigationMenuItem>
                                    <Link to="/trip" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("trip")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/raid" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("raid")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                                <NavigationMenuItem>
                                    <Link to="/feeding" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("feeding")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                            </>
                        }
                        {roles.includes("Director")  &&
                            <>
                                <NavigationMenuItem>
                                    <Link to="/reporting" >
                                        <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                            {t("reporting")}
                                        </NavigationMenuLink>
                                    </Link>
                                </NavigationMenuItem>
                            </>
                        }
                    </NavigationMenuList>
                    <NavigationMenuList className="w-full">
                        {
                            isAuth
                            ? <ChatNavItem/>
                            : <div></div>
                        }
                        <LanguageNavItem/>
                        <ThemeNavItem/>
                        {
                            isAuth
                            ? <ProfileAvatarNavigate/>
                            : <AuthNavButtons/>
                        }
                    </NavigationMenuList>
                </NavigationMenu>

            </div>
        </header>
    )
}


// (
//     <NavigationMenu className="w-full px-10 justify-between items-center flex m-auto">
//         <NavigationMenuList className="flex flex-row justify-between items-center w-full">
//             <NavigationMenuItem className="flex flex-row items-center">
//                 <img src={logo} className="size-[3rem]" alt="logo"/>
//                 <Link to="/" >
//                     <NavigationMenuLink className={navigationMenuLogoTriggerStyle()}>
//                         {t('webname')}
//                     </NavigationMenuLink>
//                 </Link>
//             </NavigationMenuItem>

// )