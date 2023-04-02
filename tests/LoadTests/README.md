# Load tests with K6

[Load testing for engineering teams | Grafana k6](https://k6.io/)

Writing tests:
- [IntelliSense](https://k6.io/docs/misc/intellisense/)
- [Load Testing Made Simpler with Resource Object Model](https://k6.io/blog/load-testing-made-simpler-with-resource-object-model/)
- [ASP.NET Core 6: Autenticación JWT y Identity Core - DEV Community](https://dev.to/isaacojeda/aspnet-core-6-autenticacion-jwt-y-identity-core-170i)
 

Runing tests:

```
cd tests\LoadTests
docker-compose up
k6 run -o influxdb=http://localhost:8086/k6 script.js
```

Test results: 

> http://localhost:4000/d/XKhgaUpika/k6-load-testing-results

Links about test results:

- [Turning data into understandable insights with K6 load testing | by Rody Bothe | Medium](https://medium.com/@rody.bothe/turning-data-into-understandable-insights-with-k6-load-testing-fa24e326e221)
- [K6 results with InfluxDB and Grafana | by Knoldus Inc. | Medium](https://medium.com/@knoldus/k6-results-with-influxdb-and-grafana-93b4dc381f6c)
- [k6 Load Testing Results | Grafana Labs](https://grafana.com/grafana/dashboards/2587-k6-load-testing-results/)
- [How to run performance tests with K6, Prometheus and Grafana](https://www.itix.fr/blog/how-to-run-performance-tests-with-k6-prometheus-grafana/)
- [szkiba/xk6-dashboard: A k6 extension that enables creating web based metrics dashboard for k6](https://github.com/szkiba/xk6-dashboard)
- [InfluxDB + Grafana](https://k6.io/docs/results-output/real-time/influxdb-grafana/)
- [k6 loves Grafana](https://k6.io/blog/k6-loves-grafana/)