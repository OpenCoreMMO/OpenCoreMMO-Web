namespace OCM.Application.Response;

[Serializable]
public class BaseResponseViewModel<T>
{
    public BaseResponseViewModel(T data)
    {
        Data = data;
    }

    public T Data { get; set; }
}