using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;


[ApiController]
[Route("[controller]")]
public class MyClassController : ControllerBase{

    private readonly ILogger<ResponceToFrontEnd> _logger;

    public MyClassController(ILogger<ResponceToFrontEnd> logger){
        _logger = logger;
    }



    [HttpGet(Name = "test")]
    public IEnumerable<ResponceToFrontEnd> Get()
    {
        List<string> res = new List<string>() {"data", "some", "data"};
        res.Add("privet");

        
        return(Enumerable.Range(1,5).Select(index => new ResponceToFrontEnd{
            data = res,
            type_of_sort = "monkey" 
        })).ToArray();

    }

    [HttpPost(Name = "api")]
    public IEnumerable<ResponceToFrontEnd> Post([FromBody] ResponceToFrontEnd req){

        //gettedArray = req.data;
        //string type_of_sort = req.type_of_sort;
        
        List<string> getedArray = new List<string>();
        getedArray = req.data; //'копирую' полученный с фронтенда массив в переменную 
        
        
        List<string> output = new List<string>(); //заранее создаю пустой список для отправки обратно на фронтэнд

        //сортировка будет происходить согласно данной последовательности
        const string a = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-!@#$%^&*() ";
        int leni, lenj, limit; //в этой переменной будет хранится длина минимального  слова из сравниваемых         
        int indexi = 0, indexj = 0, ibigest;
        string temp;


        if(req.type_of_sort == "ask"){
            //реализация сортировки пузырьком
            //логика следующая
            for(int i = 0; i < getedArray.Count - 1; i++){
                for(int j = i + 1; j < getedArray.Count; j++){
                    leni = getedArray[i].Length; //Берется длинна каждого слова(необходимо для того, чтобы если сравниваем например "аабб" и "ааб" аабб перешло в начало списка + необходимо будет циклом идти по буквам и минимальное значение понадобится)
                    lenj = getedArray[j].Length;

                    if(leni < lenj){  //выбираем нименьшую из длинн 
                        limit = leni; 
                        ibigest = 0;
                    }else{
                        limit = lenj;
                        ibigest = 1;
                    }

                    //проход по словам до длинны минимального
                    for(int k = 0; k < limit; k++){

                        // решил уже не использовать фунцкию нахождения индекса и найти индексы элеменов руками(индекс - номер позиции в алфавите)
                        for(int l = 0; l < a.Length; l++){
                            if(getedArray[i][k] == a[l]){
                                indexi = l;
                            }
                            if(getedArray[j][k] == a[l]){
                                indexj = l;
                            }
                        }

                        // если буква в слове i больше она меняется с j 
                        if(indexi > indexj){
                            temp = getedArray[i];
                            getedArray[i] = getedArray[j];
                            getedArray[j] = temp;
                            break;
                        }else if(indexj > indexi){ // если j явно больше, значит он на своем месте
                            break;
                        }else if(k == limit - 1){  // если достигнут лимит, значит необходимо сравнить 
                            if(ibigest == 1){ // длинну слов 
                                break; //если слово i длиннее оно на своем месте 
                            }else{
                                temp = getedArray[i]; // в противном случае переставляем местами 
                                getedArray[i] = getedArray[j];
                                getedArray[j] = temp;
                                break;
                            }
                        }else{
                            continue;
                        }
                    }
                }
            }
            output = getedArray;
        }else if(req.type_of_sort == "desc"){
            //ВСЕ ТО ЖЕ САМОЕ ЧТО И В МЕТОДЕ ASK
            //реализация сортировки пузырьком
            //логика следующая
            for(int i = 0; i < getedArray.Count - 1; i++){
                for(int j = i + 1; j < getedArray.Count; j++){
                    leni = getedArray[i].Length; //Берется длинна каждого слова(необходимо для того, чтобы если сравниваем например "аабб" и "ааб" аабб перешло в начало списка + необходимо будет циклом идти по буквам и минимальное значение понадобится)
                    lenj = getedArray[j].Length;

                    if(leni < lenj){  //выбираем нименьшую из длинн 
                        limit = leni; 
                        ibigest = 0;
                    }else{
                        limit = lenj;
                        ibigest = 1;
                    }

                    //проход по словам до длинны минимального
                    for(int k = 0; k < limit; k++){

                        // решил уже не использовать фунцкию нахождения индекса и найти индексы элеменов руками(индекс - номер позиции в алфавите)
                        for(int l = 0; l < a.Length; l++){
                            if(getedArray[i][k] == a[l]){
                                indexi = l;
                            }
                            if(getedArray[j][k] == a[l]){
                                indexj = l;
                            }
                        }

                        // если буква в слове i больше она меняется с j 
                        if(indexi < indexj){
                            temp = getedArray[i];
                            getedArray[i] = getedArray[j];
                            getedArray[j] = temp;
                            break;
                        }else if(indexj < indexi){ // если j явно больше, значит он на своем месте
                            break;
                        }else if(k == limit - 1){  // если достигнут лимит, значит необходимо сравнить 
                            if(ibigest == 0){ // длинну слов 
                                break; //если слово i длиннее оно на своем месте 
                            }else{
                                temp = getedArray[i]; // в противном случае переставляем местами 
                                getedArray[i] = getedArray[j];
                                getedArray[j] = temp;
                                break;
                            }
                        }else{
                            continue;
                        }
                    }
                }
            }
            output = getedArray;
        }else if(req.type_of_sort == "reverse"){
            for(int i = req.data.Count - 1; i >= 0; i-- ){
                output.Add(req.data[i]);
            }
        }
        
        return(Enumerable.Range(1,1).Select(index => new ResponceToFrontEnd{
            data = output,
            type_of_sort = "monkey"
        })).ToArray();
    }



}
