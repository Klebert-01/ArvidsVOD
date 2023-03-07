namespace VOD.Common.DTOs;

public record ClickModel(string pageType, int id);
public record RefClickModel<TRefDTO>(string pageType, TRefDTO dto); // denna är för refentiteterna?
