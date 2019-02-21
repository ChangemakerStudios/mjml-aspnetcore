using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Mjml.AspNetCore;

namespace test
{
    public class Rendering
    {
        private IMjmlServices _mjmlServices;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddMjmlServices(o =>
            {
                o.RunNpmInstall = true;
            });

            var provider = services.BuildServiceProvider();

            _mjmlServices = provider.GetRequiredService<IMjmlServices>();
        }

        [Benchmark]
        public async Task SimpleRender()
        {
            
            var result = await _mjmlServices.Render("<mjml><mj-body></mj-body></mjml>");
        }

        [Benchmark]
        public async Task ComplexRenderWithError()
        {
            var view = @"
<mjml>
    <mj-body background-color='#ccd3e0' font-size='13px'>
        <mj-section background-color='#fff' padding-bottom='20px' padding-top='20px'>
            <mj-column width='100%'>
                <mj-image src='http://go.mailjet.com/tplimg/mtrq/b/ox8s/mg1qi.png' alt='' align='center' border='none' width='100px' padding-left='0px' padding-right='0px' padding-bottom='10px' padding-top='10px'></mj-image>
                <mj-image src='http://go.mailjet.com/tplimg/mtrq/b/ox8s/mg1qz.png' alt='' align='center' border='none' width='200px' padding-left='0px' padding-right='0px' padding-bottom='0px' padding-top='0'></mj-image>
            </mj-column>
        </mj-section>
        <mj-section background-color='#356cc7' padding-bottom='0px' padding-top='0'>
            <mj-column width='100%'>
                <mj-text align='center' font-size='13px' color='#ABCDEA' font-family='Ubuntu, Helvetica, Arial, sans-serif' padding-left='25px' padding-right='25px' padding-bottom='18px' padding-top='28px'>
                    HELLO
                    <p style='font-size:16px; color:white'></p>
                </mj-text>
            </mj-column>
        </mj-section>
        <mj-section background-color='#356cc7' padding-bottom='5px' padding-top='0'>
            <mj-column width='100%'>
                <mj-divider border-color='#ffffff' border-width='2px' border-style='solid' padding-left='20px' padding-right='20px' padding-bottom='0px' padding-top='0'></mj-divider>
                <mj-text align='center' color='#FFF' font-size='13px' font-family='Helvetica' padding-left='25px' padding-right='25px' padding-bottom='28px' padding-top='28px'>
                    <span style='font-size:20px; font-weight:bold'>Thank you very much for your purchase.</span>
                    <br />
                    <span style='font-size:15px'>Please find the receipt below.</span>
                </mj-text>
            </mj-column>
        </mj-section>
        <mj-section background-color='#356CC7' padding-bottom='20px' padding-top='20px'>
            <mj-column>
                <mj-button background-color='#ffae00' color='#FFF' font-size='14px' align='center' font-weight='bold' border='none' padding='15px 30px' border-radius='10px' href='https://mjml.io' font-family='Helvetica' padding-left='25px' padding-right='25px' padding-bottom='12px'>Track My Order</mj-button>
            </mj-column>
        </mj-section>
        <mj-section background-color='#356cc7' padding-bottom='5px' padding-top='0'>
            <mj-column width='100%'>
                <mj-divider border-color='#ffffff' border-width='2px' border-style='solid' padding-left='20px' padding-right='20px' padding-bottom='0px' padding-top='0'></mj-divider>
                <mj-text align='center' color='#FFF' font-size='15px' font-family='Helvetica' padding-left='25px' padding-right='25px' padding-bottom='20px' padding-top='20px'>
                    Best,
                    <br />
                    <span style='font-size:15px'>The UCDavis TACOS Team</span>
                </mj-text>
            </mj-column>
        </mj-section>
    </mj-body>
</mjml>
";

            var result = await _mjmlServices.Render(view);
        }
    }
}
