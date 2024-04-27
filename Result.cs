public readonly struct Result<TValue>
{
    private readonly TValue? _value;
    private readonly Error? _error;

    /// <summary>
    /// For the good value
    /// </summary>
    /// <param name="value"></param>
    private Result(TValue value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }

    /// <summary>
    /// for the bad value
    /// </summary>
    /// <param name="error"></param>
    private Result(Error error)
    {
        IsError = true;
        _error = error;
        _value = default;
    }

    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Before accessing this. Check if Result is success
    /// </summary>
    public TValue Value => _value!;

    public Error Error => _error!;

    /// <summary>
    /// Will automatically detect the happy path Value and convert it into Result<TValue>
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Result<TValue>(TValue value) => new(value);

    /// <summary>
    /// Will automatically detect the bad path Value and convert it into Result<TError>
    /// </summary>
    /// <param name="error"></param>
    public static implicit operator Result<TValue>(Error error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> success, Func<Error, TResult> failure) =>
        !IsError ? success(_value!) : failure(_error!);
}