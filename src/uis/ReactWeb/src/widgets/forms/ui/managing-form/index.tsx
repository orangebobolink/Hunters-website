import {useState} from 'react';
import {User} from '@/entities/user/User.ts';
import UpdateUserDialog from '@/features/form/update-user-dialog.tsx';
import UserTableForm from '@/features/form/user-table-form.tsx';

interface IProps
{
    users: User[],
}

const ManagingForm = ({users} : IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState<User>({} as User);

    return (
        <div className="font-roboto top-0 w-full flex flex-col items-center pt-8">
            <div className="w-full max-w-4xl">
                <UserTableForm  users={users} handleClick={(user:User)=>{
                    setSelectedUser(user)
                    setIsOpen(true)
                }}/>

                <UpdateUserDialog user={selectedUser}
                                  isOpen={isOpen}
                                  setIsOpen={setIsOpen}/>
            </div>
        </div>
    );
}

export default ManagingForm;