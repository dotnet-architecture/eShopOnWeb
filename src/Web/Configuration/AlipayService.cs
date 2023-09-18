using Aop.Api;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Web.Configuration;

public static class AlipayService
{
    public static IServiceCollection AddAlipayServices(this IServiceCollection services)
    {
        services.Configure<AlipayConfig>(options =>
        {
            options.ServerUrl = "https://openapi-sandbox.dl.alipaydev.com/gateway.do";
            options.AppId = "9021000126647774";
            options.PrivateKey = @"MIIEpAIBAAKCAQEAmUyi8rz/bz4TDr4qXVLFDiXDgFd04+nqDEjU33le6jvm0Mfyt1Ca4lXNrEOsjyiSa0s9D+VqNIyct2De09tlvNhHxEzkBtzfdbcwMGyFXazMPGn/0dsy3f5b2zatJ1cvMCxXs23+/ptsCNa6UQUOX6Okss3uwCr33wiQY7vojGtaulbEM3Bk/z7OlThjMQMwaj1sf0Dk0OVeS9mb2LSq0ynpjSnInvvML+CVw93XtROL2w+dByfMXcSSy98qiC3nsTs1T7b7KV6VQp0bX+St1K7Pxfu0VIjlOIE9tt+lnoZiR13N2XNKUPIlY6VCSLeVIC0h0PzCZW+WvXy3O3tKRQIDAQABAoIBAQCAJsKpbTmTwy6nSm2b6k91Djiql3HN/YN8lX3u+0VPRpgjvKK4gBeaUeRHt6MPYflU3GbFXSE1oL55xe9A583a2mrUPPr6ibX8cBFGydGLh6KMdog25KlXTwV28fQ6SPXXrZn7E17xdPgRQ4AprrJfXSuXlfOajdD8j+o0ladHij6rS5J31KHASbJLkQhdwvya98cPqwqYMkEKe96xDC7s+MgTwNwKOL3ANKtHPDD1UJEhnj9EBSBtznLcqQBy+4JzPYhntLDsxPCU2SuN0B96BxscnupMt3dfoMsvESRtvCF3VWasmgbDH6hqBJjIgCy94zEs+3M29nBTuWmwvP0ZAoGBAOyvLplCiQSXOaGorj0yyF9ZZPobafz4llpFtRb82rScC+FndAED6nmWoJ9U56cJorL4Y6LgAyPmQXmP3+T3DZuFd/McCUkLSoC1e74BeOQ8vBmFlKPGmM/ytEFfYEP7WNZ/hK5JM49Ren3qgifFr6OsjeRnJd1kEJzEUaJH/X47AoGBAKXPX3dEpK4XPZ6KRBhfgBCT5R5/mrXZsBTG5dCZcqhCbTrJrld5U7G2VLimroc9KquMvkV8QBqDr2SU4eTQQUCUPra0slswSLUinK0UuWxoychpO5XXTPxfCsFfZJmE5WJYbXaw7T254RK0Vfn5pfzX7VW6v9tqPOpkyEs1y1F/AoGBAKEU/PbrlIseTOfcAHHALSTPl2ys2a6ElPdtN7kkb8i551AJ86Z5PsxvmnO1+k7xvNxnNpB0O07kd60/IRcYmRc+eAtqClu/Jn6AhW4kXF2hhDcudaIdGIV2Xf2b5s19s9GQSgG/6iphPqeRwfFFlqsjVhKbrOaHItM+vXxf2INfAoGAeG57x5QbDLuirPF+bjmHOgNBynoic+0OQLkKmQ2rQt0kmyt7rttX1983//pJD790q6+JT8zkfp2hGiwtOtWsX2yNdIUgeq1CUhY2uFwyJbSiwybz+Wys5S1fEX7pAbyOI3VY5Hmzhz7oZH6JaAxU7KYlIzyGN6cdYZ4cAMysxZ8CgYAb6RB0y+GH4ymFk3jx8P+00DaeqEgyttQIV5k346PvpTB65FSCjgBjYDyqiyoMrlc+9UJp2ag7PViNhESqZEeaQNGnTSspOOjwu0k9fpGr1FUK1y03Z7KB5pJeBWAIV78Z4eiQriPmtlcW/pwdKgfvGCv6Tnsry3ognGwRSlKW8A==";

            options.AlipayPublicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuswGO7VXheB+8nEmhMs2K5mi/sShdeLRYq/a75fLBPRWtBEBG/8jniFBwZTQAkGGg3j0Sh355+lR53iQvJrv0WFqC3J20n58Xoz/J6P51BTAUP/oUDlWdOlPT9lRj8XOCUOzY/z+O3aO2fH/8DZp5SHeFlDXPxJAYEypC7R4nymWLsfOEqWArcraujhkgSYbIJyd61qwv9gm/Nfi9hpyLy/Ceff7f5Sfw3avwkxEI1hDY4KWG2NAp92GyffFtHJQH/XAnDGekbzGBHa03pvumniRpZbHtK08ZqfHHo5VGMdrTFXEmm/9miJaPJb1B405uqLGFMOetaBZIVqqBSvk1wIDAQAB";

            options.SignType = "RSA2";
            options.Format = "json";
            options.Charset = "UTF-8";

        });

        services.AddScoped<DefaultAopClient>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<AlipayConfig>>().Value;
            return new DefaultAopClient(config);
        });

        return services;
    }
}
