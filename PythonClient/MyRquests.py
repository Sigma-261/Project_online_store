from prettytable import PrettyTable
import requests
import datetime 

#Comments
def getAllComments():
    response = requests.get("http://myfants.fvds.ru/api/getCommentList")
    table = PrettyTable()
    table.field_names = ["commentid", "productid", "commetntext", "commenttime"]
    for item in response.json()["response"]:
        table.add_row([str(item["commentid"]), item["productid"], item["commetntext"], item["commenttime"]])
    print(table)

def getCommentsByProductId():
    productid = input("Введите id продукта: ")
    params = {'productid': productid}
    response = requests.get("http://myfants.fvds.ru/api/getCommentListByProductId", params=params)
    table = PrettyTable()
    table.field_names = ["commentid", "productid", "commetntext", "commenttime"]
    for item in response.json()["response"]:
        table.add_row([str(item["commentid"]), item["productid"], item["commetntext"], item["commenttime"]])
    print(table)

def changeTextCommentById():
    print('Ввведите id комментария, текст которого необходимо изменить')
    idforchange = input('id: ')
    print('ИЗМЕНЕНИЕ КОММЕНТАРИЯ')
    text = input("Введите новый текст комментария: ")

    query_params = {'text': text}
    params = {'commentid': idforchange}

    try:
        response = requests.post("http://myfants.fvds.ru/api/changeCommentById", json=query_params, params=params)
        print('Был изменен текст комментария ' + idforchange + ' на '+ '\"' + text + '\"')
    except:
        print('Произошла ошибка изменения комментария')

def addComment():
    print('Ввведите id продукта')
    product_id = input('id продукта: ')
    print('Ввведите текст комментария')
    text = input('Текст: ')
    print('Дата установится автоматически')

    query_params = {'productid': product_id, 'text': text}
    try:
        response = requests.post("http://myfants.fvds.ru/api/addComment", json=query_params)
        print('Был добавлен комментарий к товару ' + product_id + ' текст: '+ '\"' + text + '\"')
    except:
        print('Произошла ошибка добавления комментария')

def RemoveComment():
    print('Ввведите id комментария, который необходимо удалить')
    commentid = input('id комментария: ')
    params = {'commentid': commentid}

    try:
        response = requests.delete("http://myfants.fvds.ru/api/removeCommentById", params=params)
        print('Был удален комментарий с id: ' + commentid)
    except:
        print('Произошла ошибка удаления комментария')

#ORDERS
def getOrdersWithUsers():
    response = requests.get("http://myfants.fvds.ru/api/getOrdersWithUsers")
    table = PrettyTable()
    table.field_names = ["ID", "PRICE", "DATETIME", "IS_USER", "LOGIN_USER"]
    for item in response.json()["response"]:
        table.add_row([str(item["ID"]), str(item["PRICE"]), str(item["DATATIME"]), str(item["ID_USER"]), item["LOGIN_USER"]])
    print(table)
#Products
def setProductCategory():
    print('Ввведите id товара, котегорию которого необходимо добавить')
    productid = input('id товара: ')
    print('Ввведите название категории, которую необходимо добавить товару')
    categoryname = input('Навзание: ')

    params = {'productid': productid, 'categoryname': categoryname}

    response = requests.post("http://myfants.fvds.ru/api/changeProductByIdCategoryByName", params=params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
        print('Была добавлена товару категория:' + categoryname)
        print('Название: ')
    else:
        print('Произошла ошибка добавления категории товару(см. выше)')

def getProductsWithCategoryWithSeller():
    response = requests.get("http://myfants.fvds.ru/api/getProductsWithCategoryWithSeller")
    table = PrettyTable()
    table.field_names = ["id", "name", "price", "date_time", "description", "category", "sellerid", "sellerLOGIN"]
    for item in response.json()["response"]:
        table.add_row([str(item["id"]), item["name"], str(item["price"]), item["date_time"][:10], 
                        item["description"], item["category"], str(item["sellerid"]), item["sellerLOGIN"]])
    print(table)

def getProducts():
    response = requests.get("http://myfants.fvds.ru/api/getProducts")
    table = PrettyTable()
    table.field_names = ["id", "name", "price", "date_time", "description", "sellerid"]
    for item in response.json()["response"]:
        table.add_row([str(item["id"]), item["name"], str(item["price"]), item["date_time"][:10], item["description"], str(item["sellerid"])])
    print(table)

def addProduct():
    print('Ввведите название продукта')
    product_name = input('Название: ')
    print('Ввведите цену товара')
    product_price = input('Цена: ')
    print('Дата установится автоматически')
    now = datetime.datetime.now()
    product_date = now.strftime('%m-%d-%Y')
    print('Ввведите описание товара')
    product_description = input('Описание: ')
    print('Ввведите id пользователя-продавца')
    product_sellerid = input('id продавца: ')

    query_params = {'name': product_name, 'price': product_price, 'date_time': product_date, 'description': product_description, 'sellerid': product_sellerid}
    response = requests.post("http://myfants.fvds.ru/api/addProduct", json=query_params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
            print('Был добавлен товар:')
            print('Название: ' + product_name, 'Цена: ' + product_price, 'Дата создания: '+ product_date, 'Описание: ' + product_description, 'id продавца: ' + product_sellerid, sep='\n')
    else:
        print('Произошла ошибка добавления пользователя(см. выше)')

def RemoveProduct():
    print('Ввведите id товара, который необходимо удалить')
    id_remove = input('id товара: ')
    params = {'id': id_remove, 'pass': '196574839'}

    response = requests.delete("http://myfants.fvds.ru/api/removeProductById", params=params)
    print(response.json())
    print(response.status_code)
    if response.status_code == 200:
        print('Был удален товар с id: ' + id_remove)

def ChangeProduct():
    print('Ввведите id товара, который необходимо изменить')
    idforchange = input('id: ')
    print('ИЗМЕНЕНИЕ ТОВАРА')
    print('Ввведите название продукта')
    product_name = input('Название: ')
    print('Ввведите цену товара')
    product_price = input('Цена: ')
    print('Введите дату в формате месяц-день-год(Например: 01-01-2021')
    product_date = input('Дата: ')
    print('Ввведите описание товара')
    product_description = input('Описание: ')
    print('Ввведите id пользователя-продавца')
    product_sellerid = input('id продавца: ')

    query_params = {'name': product_name, 'price': product_price, 'date_time': product_date, 'description': product_description, 'sellerid': product_sellerid}
    params = {'id': idforchange}

    response = requests.post("http://myfants.fvds.ru/api/changeProductById", json=query_params, params=params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
        print('Был изменен товар:')
        print('Название: ' + product_name, 'Цена: ' + product_price, 'Дата создания: '+ product_date, 'Описание: ' + product_description, 'id продавца: ' + product_sellerid, sep='\n')
    else:
        print('Произошла ошибка изменения товара(см. выше)')
#Categories
def getCategories():
    response = requests.get("http://myfants.fvds.ru/api/getCategories")
    table = PrettyTable()
    table.field_names = ["id", "name"]
    for item in response.json()["response"]:
        table.add_row([str(item["id"]), item["name"]])
    print(table)

def addCategory():
    #Проверка существутет ли уже такая категория
    isExist = True
    while isExist == True:
        print('Ввведите название категории')
        category_name = input('Название: ')
        print('Категория с таким именем уже существует, введите другую!')  
        response = requests.get("http://myfants.fvds.ru/api/getCategories")
        for item in response.json()["response"]:
            isExist = category_name == item["name"]
    query_params = {'name': category_name}
    response = requests.post("http://myfants.fvds.ru/api/addCategory", json=query_params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
            print('Была добавлена категория:')
            print('Название: '+ category_name)
    else:
        print('Произошла ошибка добавления категории(см. выше)')

def removeCategory():
    print('Ввведите название категории, которую необходимо удалить')
    name = input('Название: ')
    password = '196574839'
    params = {'name': name, 'pass': password}

    response = requests.delete("http://myfants.fvds.ru/api/removeCategory", params=params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
        print('Была удалена категория: ' + name)

def ChangeCategory():
    print('Ввведите название категории, которую необходимо изменить')
    name = input('Название: ')
    print('ИЗМЕНЕНИЕ Категории')
    print('Ввведите название категории')
    changed_name = input('Название: ')

    query_params = {'name': changed_name}
    params = {'name': name}

    response = requests.post("http://myfants.fvds.ru/api/changeCategoryByName", json=query_params, params=params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
        print('Была изменена категория:' + changed_name)
        print('Название: ')
    else:
        print('Произошла ошибка изменения категории(см. выше)')

#USERS
def getUsers():
    response = requests.get("http://myfants.fvds.ru/api/getUsers")
    table = PrettyTable()
    table.field_names = ["ID", "LOGIN", "PASSWORD", "IS_ADMIN"]
    for item in response.json()["response"]:
        table.add_row([str(item["ID"]), item["LOGIN"], item["PASSWORD"], str(item["IS_ADMIN"])])
    print(table)

def addUser():
    print('Ввведите логин пользователя')
    USER_LOGIN = input('Логин: ')
    print('Ввведите пароль пользователя')
    USER_PASSWORD = input('Пароль: ')
    print('Ввведите статус администрирования пользователя в формате \"True\" или \"False\" без кавычек')
    USER_IS_ADMIN = input('Администратор: ')
    while USER_IS_ADMIN != 'True' and USER_IS_ADMIN != 'False':
        print('Необходим формат \"True\" или \"False\" без кавычек')
        USER_IS_ADMIN = input('Администратор: ')

    query_params = {'LOGIN': USER_LOGIN, 'PASSWORD': USER_PASSWORD, 'IS_ADMIN': USER_IS_ADMIN}

    response = requests.post("http://myfants.fvds.ru/api/addUser", json=query_params)
    print(response.json())
    print(response.status_code)
    if response.json()['status'] != 'FAIL' and response.status_code!=400:
            print('Был добавлен пользователь:')
            print('Логин: '+ USER_LOGIN, 'Пароль: ' + USER_PASSWORD, 'Администратор: ' + USER_IS_ADMIN, sep='\n')
    else:
        print('Произошла ошибка добавления пользователя(см. выше)')

def ChangeUser():
    while (True):
        print('1) Изменять по логину')
        print('2) Изменять по id')
        typechange = input("Значение:")
        if typechange == '1':
            print('Ввведите логин пользователя, которого необходимо изменить')
            loginforchange = input('Логин: ')
            print('ИЗМЕНЕНИЕ ПОЛЬЗОВАТЕЛЯ')
            print('Ввведите логин пользователя')
            USER_LOGIN = input('Логин: ')
            print('Ввведите пароль пользователя')
            USER_PASSWORD = input('Пароль: ')
            print('Ввведите статус администрирования пользователя в формате \"True\" или \"False\" без кавычек')
            USER_IS_ADMIN = input('Администратор: ')
            while USER_IS_ADMIN != 'True' and USER_IS_ADMIN != 'False':
                print('Необходим формат \"True\" или \"False\" без кавычек')
                USER_IS_ADMIN = input('Администратор: ')

            query_params = {'LOGIN': USER_LOGIN, 'PASSWORD': USER_PASSWORD, 'IS_ADMIN': USER_IS_ADMIN}
            params = {'loginforchange': loginforchange}

            response = requests.post("http://myfants.fvds.ru/api/ChangeUserByName", json=query_params, params=params)
            print(response.json())
            print(response.status_code)
            if response.json()['status'] != 'FAIL' and response.status_code!=400:
                print('Был изменен пользователь:')
                print('Логин: '+ USER_LOGIN, 'Пароль: ' + USER_PASSWORD, 'Администратор: ' + USER_IS_ADMIN, sep='\n')
            else:
                print('Произошла ошибка изменения пользователя(см. выше)')
            break
        elif typechange == '2':
            print('Ввведите ID пользователя, которого необходимо изменить(число)')
            idforchange = input('Id: ')
            print('ИЗМЕНЕНИЕ ПОЛЬЗОВАТЕЛЯ')
            print('Ввведите логин пользователя')
            USER_LOGIN = input('Логин: ')
            print('Ввведите пароль пользователя')
            USER_PASSWORD = input('Пароль: ')
            print('Ввведите статус администрирования пользователя в формате \"True\" или \"False\" без кавычек')
            USER_IS_ADMIN = input('Администратор: ')
            while USER_IS_ADMIN != 'True' and USER_IS_ADMIN != 'False':
                print('Необходим формат \"True\" или \"False\" без кавычек')
                USER_IS_ADMIN = input('Администратор: ')

            query_params = {'LOGIN': USER_LOGIN, 'PASSWORD': USER_PASSWORD, 'IS_ADMIN': USER_IS_ADMIN}
            params = {'idforchange': idforchange}

            response = requests.post("http://myfants.fvds.ru/api/ChangeUserById", json=query_params, params=params)
            print(response.json())
            print(response.status_code)
            if response.json()['status'] != 'FAIL' and response.status_code!=400:
                print('Был изменен пользователь:')
                print('Логин: '+ USER_LOGIN, 'Пароль: ' + USER_PASSWORD, 'Администратор: ' + USER_IS_ADMIN, sep='\n')
            else:
                print('Произошла ошибка изменения пользователя(см. выше)')
            break
        else:
            print("Неизвестное значение")

def RemoveUser():
    while (True):
        print('1) Удалить по логину(Удалится первый в списке)')
        print('2) Удалить по id')
        typechange = input("Значение:")
        if typechange == '1':
            print('Ввведите логин пользователя, которого необходимо удалить')
            loginforremove = input('Логин: ')
            params = {'loginforremove': loginforremove}

            response = requests.delete("http://myfants.fvds.ru/api/removeUserByName", params=params)
            print(response.json())
            print(response.status_code)
            if response.status_code == 200:
                print('Был удален пользователь с логином: ' + loginforremove)
            break
        elif typechange == '2':
            print('Ввведите id пользователя, которого необходимо удалить')
            idforremove = input('id: ')
            print(idforremove)
            params = {'idforremove': idforremove}

            response = requests.delete("http://myfants.fvds.ru/api/removeUserByid", params=params)
            print(response.json())
            print(response.status_code)
            if response.json()['status'] != 'FAIL' and response.status_code!=400:
                print('Был удален пользователь с id: ' + idforremove)
            else:
                print('Произошла ошибка удаления пользователя(см. выше)')
            break
        else:
            print("Неизвестное значение")
