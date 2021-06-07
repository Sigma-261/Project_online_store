from MyRquests import ChangeCategory, ChangeProduct, ChangeUser, RemoveProduct, RemoveUser, addCategory, addProduct, addUser, getCategories, getProducts, getProductsWithCategoryWithSeller, getUsers, removeCategory, setProductCategory

def main():

    while True:
        print('----ГЛАВНОЕ МЕНЮ ПРОГРАММЫ----')
        print('Выберите действие')
        print('1) Администрирование ')
        print('2) Просмотр данных ')
        print('\nexit для закрытия программы')
        action = input()

        if action =='1':
            Administration()
            return
        elif action == '2':
            Showing()
            return
        elif action == 'exit':
            exit
            return
        else:
            print('Неизвестное значение')

def Administration():
    while True:
        print('----АДМИНИСТРИРОВАНИЕ----')
        print('Выберите действие')
        print('1) Добавить пользователя')
        print('2) Изменить пользователя')
        print('3) Удалить пользователя')
        print('4) Добавить категорию')
        print('5) Изменить категорию')
        print('6) Удалить категорию')
        print('7) Добавить товар')
        print('8) Удалить товар')
        print('9) Изменить товар')
        print('10) Добавить категорию товара')
        print('\nexit для закрытия программы')
        print('return для возвращения в главное меню программы')
        action = input()

        if action=='1':
            addUser()
            input("Нажмите ВВОД для продолжения...")
        elif action=='2':
            ChangeUser()
            input("Нажмите ВВОД для продолжения...")
        elif action=='3':
            RemoveUser()
            input("Нажмите ВВОД для продолжения...")
        elif action=='4':
            addCategory()
            input("Нажмите ВВОД для продолжения...")
        elif action=='5':
            ChangeCategory()
            input("Нажмите ВВОД для продолжения...")
        elif action=='6':
            removeCategory()
            input("Нажмите ВВОД для продолжения...")
        if action =='7':
            addProduct()
            input("Нажмите ВВОД для продолжения...")
        if action =='8':
            RemoveProduct()
            input("Нажмите ВВОД для продолжения...")
        if action =='9':
            ChangeProduct()
            input("Нажмите ВВОД для продолжения...")
        if action =='10':
            setProductCategory()
            input("Нажмите ВВОД для продолжения...")
        elif action == 'exit':
            exit
            return
        elif action == 'return':
            main()
            return
        else:
            print('Неизвестное значение')

def Showing():
    while True:
        print('----ПРОСМОТР ДАННЫХ----')
        print('Выберите действие')
        print('1) Просмотреть все товары ')
        print('2) Просмотреть все товары, включив категорию и id продавца')
        print('3) Просмотреть все категории ')
        print('4) Просмотреть всех пользователей ')
        print('\nexit для закрытия программы')
        print('return для возвращения в главное меню программы')
        action = input()
        
        if action =='1':
            getProducts()
            input("Нажмите ВВОД для продолжения...")
        elif action =='2':
            getProductsWithCategoryWithSeller()
            input("Нажмите ВВОД для продолжения...")
        elif action =='3':
            getCategories()
            input("Нажмите ВВОД для продолжения...")
        elif action =='4':
            getUsers()
            input("Нажмите ВВОД для продолжения...")
        elif action == 'exit':
            exit
            return
        elif action == 'return':
            main()
            return

if __name__ == '__main__':
    main()