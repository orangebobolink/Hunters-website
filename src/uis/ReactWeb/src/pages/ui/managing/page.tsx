import {useEffect, useState} from 'react';
import ManagingForm from '@/widgets/forms/ui/managing-form';
import {UserService} from '@/entities/user/UserService.ts';
import {User} from '@/entities/user/User.ts';
import AddUserDialog from '@/features/dialog/add-user-dialog.tsx';
import {Button} from '@/shared/ui';
import {useTranslation} from 'react-i18next';

const ManagingPage = () => {
    const [users, setUsers] = useState<User[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const [count, setCount] = useState(0);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "managing"
        });

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await UserService.getAll();
                setUsers(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, [count]);

    return (
        <div className="select-none h-full flex items-center flex-col justify-start">
            <ManagingForm users={users}/>
            <div className="flex mt-[6vh] w-full flex-row justify-around">
                <div></div>
                <Button onClick={()=>setIsOpen(true)}>{t("createUser")}</Button>
            </div>

            <AddUserDialog isOpen={isOpen}
                           setIsOpen={setIsOpen}
                           increaseCount={() => setCount(count + 1)}/>
        </div>
    );
};

export default ManagingPage;